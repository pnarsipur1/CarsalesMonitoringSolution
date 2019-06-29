using MonitoringSite.Helpers;
using SharedLibrary;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace MonitoringSite.Controllers
{
    public class GetRabbitMqDataController : ApiController
    {
        private AppSettings _appSettings;

        public GetRabbitMqDataController()
        {
            _appSettings = new AppSettings();
        }

        [HttpGet]
        [Route("api/GetLiveMessageCount/{queueName}")]
        public IHttpActionResult GetLiveMessageCount(string queueName)
        {
            RabbitMqRepository rabbitMqRepo = new RabbitMqRepository(_appSettings.RabbitMqUsername,
                _appSettings.RabbitMqPassword,
                _appSettings.RabbitMqVirtualHost,
                _appSettings.RabbitMqHost);

            var obj= rabbitMqRepo.GetMessageCount(_appSettings.RabbitMqExchange, queueName);

            return Ok(new { results = obj });
        }

        [HttpGet]
        [Route("api/GetLiveMessageCount/{queueName}/{startDateString}/{endDateString}")]
        public IHttpActionResult GetHistoryMessageCount(string queueName, string startDateString, string endDateString)
        {
            var startDate = BuildDateTimeFromYAFormat(startDateString);
            var endDate = BuildDateTimeFromYAFormat(endDateString);
            DbRepository dbRepository = new DbRepository();
            var obj = dbRepository.GetMessageCountFromQueueHistory(_appSettings.MonitoringDbConnectionString, startDate, endDate, queueName);
            return Ok(new { results = obj });
        }

        /// <summary>
        /// Convert a UTC Date String of format yyyyMMddThhmmZ into a Local Date
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        private DateTime BuildDateTimeFromYAFormat(string dateString)
        {
            Regex r = new Regex(@"^\d{4}\d{2}\d{2}T\d{2}\d{2}Z$");
            if (!r.IsMatch(dateString))
            {
                throw new FormatException(
                    string.Format("{0} is not the correct format. Should be yyyyMMddThhmmZ", dateString));
            }

            DateTime dt = DateTime.ParseExact(dateString, "yyyyMMddThhmmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            return dt;
        }

    }
}
