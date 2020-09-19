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
using Phonebook.Models.PhoneNumber;
using Phonebook.Models.Location;

namespace Phonebook.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PhoneNumbersWebApiController : Controller
    {
        private PhonebookContext _context;

        public PhoneNumbersWebApiController(PhonebookContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var phonenumbers = _context.PhoneNumbers.Select(i => new {
                i.Id,
                i.DistrictId,
                i.MicrodistrictId,
                i.Address,
                i.FullName,
                i.Number,
                i.Note
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(phonenumbers, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new PhoneNumber();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.PhoneNumbers.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.PhoneNumbers.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.PhoneNumbers.FirstOrDefaultAsync(item => item.Id == key);

            _context.PhoneNumbers.Remove(model);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        public object DistrictLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Locations.OfType<District>()
                         let text = i.Name 
                         orderby i.Name
                         select new
                         {
                             Value = i.ID,
                             Text = text
                         };

            return DataSourceLoader.Load(lookup, loadOptions);
        }

        [HttpGet]
        public object MicrodistrictLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Locations.OfType<Microdistrict>()
                         let text = i.Name
                         orderby i.Name
                         select new
                         {
                             Value = i.ID,
                             Text = text
                         };

            return DataSourceLoader.Load(lookup, loadOptions);
        }

        private void PopulateModel(PhoneNumber model, IDictionary values) {
            string ID = nameof(PhoneNumber.Id);
            string DISTRICT_ID = nameof(PhoneNumber.DistrictId);
            string MICRODISTRICT_ID = nameof(PhoneNumber.MicrodistrictId);
            string ADDRESS = nameof(PhoneNumber.Address);
            string FULL_NAME = nameof(PhoneNumber.FullName);
            string NUMBER = nameof(PhoneNumber.Number);
            string NOTE = nameof(PhoneNumber.Note);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(DISTRICT_ID)) {
                model.DistrictId = values[DISTRICT_ID] != null ? Convert.ToInt32(values[DISTRICT_ID]) : (int?)null;
            }

            if(values.Contains(MICRODISTRICT_ID)) {
                model.MicrodistrictId = values[MICRODISTRICT_ID] != null ? Convert.ToInt32(values[MICRODISTRICT_ID]) : (int?)null;
            }

            if(values.Contains(ADDRESS)) {
                model.Address = Convert.ToString(values[ADDRESS]);
            }

            if(values.Contains(FULL_NAME)) {
                model.FullName = Convert.ToString(values[FULL_NAME]);
            }

            if(values.Contains(NUMBER)) {
                model.Number = Convert.ToString(values[NUMBER]);
            }

            if(values.Contains(NOTE)) {
                model.Note = Convert.ToString(values[NOTE]);
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
    }
}