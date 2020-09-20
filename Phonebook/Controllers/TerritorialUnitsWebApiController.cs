using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Phonebook.Models;
using Phonebook.Models.Location;

namespace Phonebook.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TerritorialUnitsWebApiController : Controller
    {
        private readonly PhonebookContext _context;

        public TerritorialUnitsWebApiController(PhonebookContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDistricts(DataSourceLoadOptions loadOptions) {
            var locations = _context.Locations.OfType<District>().Select(i => new {
                i.ID,
                i.Name
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(locations, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetMicrodistricts(DataSourceLoadOptions loadOptions)
        {
            var locations = _context.Locations.OfType<Microdistrict>().Select(i => new {
                i.ID,
                i.Name,
                i.DistrictId
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(locations, loadOptions));
        }

        [HttpPost]
        public Task<IActionResult> PostDistrict(string values)
        {
            var model = new District();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);
            return Post(model);
        }

        [HttpPost]
        public Task<IActionResult> PostMicrodistrict(string values)
        {
            var model = new Microdistrict();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);
            return Post(model);
        }
        public async Task<IActionResult> Post(TerritorialUnit model) {
            
            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Locations.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ID });
        }

        [HttpPut]
        public async Task<IActionResult> PutDistrict(int key, string values) {
            District model = await _context.Locations.OfType<District>().FirstOrDefaultAsync(item => item.ID == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            return await Put(model);
        }

        [HttpPut]
        public async Task<IActionResult> PutMicrodistrict(int key, string values)
        {
            Microdistrict model = await _context.Locations.OfType<Microdistrict>().FirstOrDefaultAsync(item => item.ID == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            return await Put(model);
        }
        public async Task<IActionResult> Put(TerritorialUnit model) {
            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Locations.FirstOrDefaultAsync(item => item.ID == key);

            _context.Locations.Remove(model);
            await _context.SaveChangesAsync();
        }

        private void PopulateModel(Microdistrict model, IDictionary values)
        {
            PopulateTerritorialUnitModel(model, values);

            string DISTRICT_ID = nameof(Microdistrict.DistrictId);

            if (values.Contains(DISTRICT_ID))
            {
                model.DistrictId = values[DISTRICT_ID] != null ? Convert.ToInt32(values[DISTRICT_ID]) : (int?)null;
            }
        }
        private void PopulateModel(District model, IDictionary values)
        {
            PopulateTerritorialUnitModel(model, values);
        }

        private void PopulateTerritorialUnitModel(TerritorialUnit model, IDictionary values) {
            string ID = nameof(TerritorialUnit.ID);
            string NAME = nameof(TerritorialUnit.Name);

            if (values.Contains(ID)) {
                model.ID = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }

        [HttpPost]
        public async Task<JsonResult> CheckUniqueName([FromBody] UniqueViewModel data)
        {
            TerritorialUnit unit = await _context.Locations.FirstOrDefaultAsync(u => u.Name == data.UniqueText);
            bool isValid = unit == null || data.ID == unit.ID;
            return Json(isValid);
        }
    }
}