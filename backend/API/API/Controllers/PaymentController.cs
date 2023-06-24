using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost("create-payment-intent")]
        public ActionResult CreatePaymentIntent([FromBody] PaymentIntentCreateRequest request)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51NJ0V8KIQvqZ1VxJMMjKG4nORzlwpiS7jpG54FB4DBUPwur4gDe0763QSpnvPbD0sOHnELcaObwF77TbyRDMlNLd00qm9CR0Sw";  // Replace with your actual secret key

                var options = new PaymentIntentCreateOptions
                {
                    Amount = CalculateOrderAmount(request.Items),
                    Currency = "RON",
                };

                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);
                Console.WriteLine("Client Secret: " + paymentIntent.ClientSecret);  // Log the client secret
                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("markAsPaid/{id}")]
        public ActionResult MarkAsPaid(string id)
        {
            try
            {
                

                return Ok(new { status = "Invoice marked as paid" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest(new { error = ex.Message });
            }
        }



        private int CalculateOrderAmount(Item[] items)
        {

            return items.Sum(item => item.Amount);
        }
    }

    public class PaymentIntentCreateRequest
    {
        public Item[] Items { get; set; }
    }

    public class Item
    {
        public string Id { get; set; }
        public int Amount { get; set; }
    }




}

