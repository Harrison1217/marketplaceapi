﻿using Marketplace.Models;
using Marketplace.Models.Customer;
using Marketplace.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Marketplace.WebApi.Controllers
{
   
    public class CustomerController : ApiController
    {
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAll()
        {
            CustomerService customerService = CreateCustomerService();
            var customers = customerService.GetAllCustomers();
            return Ok(customers);
        }

        public IHttpActionResult Get(int id)
        {
            CustomerService customerService = CreateCustomerService();
            var customer = customerService.GetCustomerById(id);
            return Ok(customer);
        }

        public IHttpActionResult Post(CustomerCreate customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCustomerService();

            if (!service.CreateCustomer(customer))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(CustomerEdit customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCustomerService();

            if (!service.UpdateCustomer(customer))
                return InternalServerError();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCustomerService();

            if (!service.DeleteCustomer(id))
                return InternalServerError();

            return Ok();
        }

        private CustomerService CreateCustomerService()
        {
            var ownerId = Guid.Parse(User.Identity.GetUserId());
            var service = new CustomerService(ownerId);
            return service;
        }
    }
}
