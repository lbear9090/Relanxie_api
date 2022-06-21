using API.Models.CCAvenue;
using CCA.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment

        string AccessCode = ConfigurationManager.AppSettings["CcAvenueAccessCode"];
        string CheckoutUrl = ConfigurationManager.AppSettings["CcAvenueCheckoutUrl"];
        string WorkingKey = ConfigurationManager.AppSettings["CcAvenueWorkingKey"];
        string MerchantId = ConfigurationManager.AppSettings["CcAvenueMerchantId"];

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(string invoiceNumber)
        {
            string amount = "500";
            var queryParameter = new CCACrypto();

            //CCACrypto is the dll you get when you download the ASP.NET 3.5 integration kit from //ccavenue account.

            return View("CcAvenue", new CcAvenueViewModel(queryParameter.Encrypt
           (BuildCcAvenueRequestParameters(invoiceNumber, amount), WorkingKey), AccessCode, CheckoutUrl));
        }

        [HttpPost]
        public ActionResult PaymentSuccessful(string encResp)
        {
            var decryption = new CCACrypto();
            var decryptedParameters = decryption.Decrypt(encResp, WorkingKey);

            var keyValuePairs = decryptedParameters.Split('&');
            var splittedKeyValuePairs = new Dictionary<string, string>();

            foreach (var value in keyValuePairs)
            {
                var keyValuePair = value.Split('=');
                splittedKeyValuePairs.Add(keyValuePair[0], keyValuePair[1]);
            }

            //Here you can check the consistency of data i.e what you send is what you get back,
            //Make sure its not corrupted....
            //After that Save the details of the transaction into a db if you want to...
            //I am just returning the data I got back...

            return View(splittedKeyValuePairs);
        }

        [HttpPost]
        public ActionResult PaymentCancelled()
        {
            return View();
        }
        private string BuildCcAvenueRequestParameters(string invoiceNumber, string amount)
        {

            var queryParameters = new Dictionary<string, string>
             {
             {"order_id", invoiceNumber},
             {"merchant_id", MerchantId},
             {"amount", amount},
             {"currency","INR" },
             {"redirect_url","/PaymentSuccessful" },
             {"cancel_url","/PaymentCancelled"},
             {"request_type","JSON" },
             {"response_type","JSON" },
             {"version","1.1" }
        }.Select(item => string.Format("{0}={1}", item.Key, item.Value));
            return string.Join("&", queryParameters);
        }

        // GET: Payment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
