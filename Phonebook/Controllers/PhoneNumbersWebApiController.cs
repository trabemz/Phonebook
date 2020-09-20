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

namespace Phonebook.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PhoneNumbersWebApiController : Controller
    {
        private readonly PhonebookContext _context;
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public PhoneNumbersWebApiController(PhonebookContext context, IPhoneNumberRepository phoneNumberRepository) {
            _context = context;
            _phoneNumberRepository = phoneNumberRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var phonenumbers = _phoneNumberRepository.GetAll();

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

            var resultId = await _phoneNumberRepository.Create(model);

            return Json(new { resultId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _phoneNumberRepository.GetById(key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _phoneNumberRepository.Update();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            await _phoneNumberRepository.DeleteById(key);
        }

        private void PopulateModel(PhoneNumber model, IDictionary values) {
            string ID = nameof(PhoneNumber.ID);
            string DISTRICT_ID = nameof(PhoneNumber.DistrictId);
            string MICRODISTRICT_ID = nameof(PhoneNumber.MicrodistrictId);
            string ADDRESS = nameof(PhoneNumber.Address);
            string FULL_NAME = nameof(PhoneNumber.FullName);
            string NUMBER = nameof(PhoneNumber.Number);
            string NOTE = nameof(PhoneNumber.Note);

            if(values.Contains(ID)) {
                model.ID = Convert.ToInt32(values[ID]);
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

        [HttpPost]
        public async Task<JsonResult> CheckUniquePhoneNumber([FromBody] PhoneNumberViewModel data)
        {
            bool isValid = await _phoneNumberRepository.CheckIfNumberUnique(data.id, data.number);
            return Json(isValid);
        }

    }
}