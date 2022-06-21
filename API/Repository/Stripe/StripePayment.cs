using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Avigma.Repository.Lib;
using System.Configuration;

namespace Avigma.Repository.Stripe
{
    public class StripePayment
    {
        Log log = new Log();
        public void AddCustomerAccountByAccountID(string CustEmail,string StrConnected_Account_ID)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeKey"]; // "{{CONNECTED_ACCOUNT_SECRET_KEY}}";
                var customerOptions = new CustomerCreateOptions
                {
                    Email = CustEmail
                };
                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = StrConnected_Account_ID; // "{{CONNECTED_ACCOUNT_ID}}";
                var customerService = new CustomerService();
                Customer customer = customerService.Create(customerOptions, requestOptions);
                 // Fetching an account just needs the ID as a parameter
                 var accountService = new AccountService();
                Account account = accountService.Get(ConfigurationManager.AppSettings["StripeAccountID"]); //"{{CONNECTED_STRIPE_ACCOUNT_ID}}"
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
           
        }

        public string AddCustomerAccountByAPIKey(string CustEmail, string srcid /*User_MasterDTO user_MasterDTO*/)
        {
            string userid = string.Empty;
            try
            {
                StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings["StripeKey"]; // "{{CONNECTED_ACCOUNT_SECRET_KEY}}";
                if(srcid==null || srcid=="")
                {
                    var customerOptions = new CustomerCreateOptions
                    {
                        Email = CustEmail,//user_MasterDTO.Um_Email
                        Description = "Check subscriptions for " + CustEmail
                    };

                    var requestOptions = new RequestOptions();
                    requestOptions.ApiKey = ConfigurationManager.AppSettings["StripeKey"]; // "{{CONNECTED_ACCOUNT_SECRET_KEY}}";
                    var customerService = new CustomerService();
                    Customer customer = customerService.Create(customerOptions);
                    userid = customer.Id;
                }
                else
                {
                    var customerOptions = new CustomerCreateOptions
                    {
                        Email = CustEmail,//user_MasterDTO.Um_Email
                        Source = srcid,
                        Description = "Check subscriptions for " + CustEmail
                    };
                    var requestOptions = new RequestOptions();
                    requestOptions.ApiKey = ConfigurationManager.AppSettings["StripeKey"]; // "{{CONNECTED_ACCOUNT_SECRET_KEY}}";
                    var customerService = new CustomerService();
                    Customer customer = customerService.Create(customerOptions);
                    userid = customer.Id;
                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return userid;

        }

        public string CreateSubscription(String CustomerID ,String Price)
        {
            string strSubsID = string.Empty;
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeKey"]; // "{{CONNECTED_ACCOUNT_SECRET_KEY}}";

                string price= ConfigurationManager.AppSettings["Price"];
                string CuustId = ConfigurationManager.AppSettings["CustomerId"];
                var options = new SubscriptionCreateOptions
                {
                    Customer = CustomerID, //CustomerID,//"cus_I26yTDqpd4ko0Q",
                    Items = new List<SubscriptionItemOptions>
                     {
                         new SubscriptionItemOptions
                        {
                          Price =price //price,//"price_1HSKb2HRZrM6MDOWjdYqne8n",
                        },
                      },
                };
                var service = new SubscriptionService();
                var SubscriptionData = service.Create(options);
                strSubsID = SubscriptionData.Id;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return strSubsID;
        }

          
        public string GetCustomerDetails(String Email, string srcid)
        {
            string userid = string.Empty;
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeKey"];

                var options = new CustomerListOptions
                {
                    Limit = 3,
                    Email = Email
                };
                var service = new CustomerService();
                StripeList<Customer> customers = service.List(
                  options
                );

                if(customers.Count() > 0) { 
                userid = customers.Data[0].Id; //by pradeep
                }
                else 
                {
                    userid= AddCustomerAccountByAPIKey(Email, srcid);
                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return userid;
        }


        public string CutPayment(string custToken,string Name,Int64 paymentAmount)
        {
            string PaymentId = string.Empty;
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeKey"];
                
                var options = new ChargeCreateOptions()
                {
                    Amount = paymentAmount,
                    Currency = "usd",
                    Source = custToken,//"tok_amex",
                    Metadata = new Dictionary<string, string>
                {
                        //{ "OrderId", "6735" },
                         { "OrderId",Name },
                }
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);
                PaymentId = charge.Id;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return PaymentId;
        }

        public string CancelSubscrition(string custSubCriptionID)
        {
            string StrID = string.Empty; 
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeKey"];

                var service = new SubscriptionService();
                //service.Cancel("sub_I26yjjluvzM77K");
               var cancel =  service.Cancel(custSubCriptionID);
                StrID = cancel.Id;
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return StrID;
        }

        public string AttachPaymentMethodToCustomer(string PaymentId,string CustomerID )
        {
            string StrID = string.Empty;
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeKey"];

                var options = new PaymentMethodAttachOptions
                {
                    Customer = CustomerID,
                };
                var service = new PaymentMethodService();
               var attach= service.Attach(
                  PaymentId,
                  options
                );
                StrID = attach.Id;


            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return StrID;
        }

    }
}