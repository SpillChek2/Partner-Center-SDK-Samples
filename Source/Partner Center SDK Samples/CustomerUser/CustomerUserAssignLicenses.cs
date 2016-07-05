// -----------------------------------------------------------------------
// <copyright file="CustomerUserAssignLicenses.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Samples.CustomerUser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Models.Licenses;

    /// <summary>
    /// Assign customer user a license.
    /// </summary>
    public class CustomerUserAssignLicenses : BasePartnerScenario
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserAssignLicenses"/> class.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        public CustomerUserAssignLicenses(IScenarioContext context) : base("Assign customer user a license", context)
        {
        }

        /// <summary>
        /// Executes the assign customer user a license scenario.
        /// </summary>
        protected override void RunScenario()
        {
            // get customer user Id.
            string selectedCustomerUserId = this.ObtainCustomerUserId("Enter the ID of the customer user to assign license");

            // get customer Id of the entered customer user.
            string selectedCustomerId = this.ObtainCustomerId("Enter the ID of the customer");

            var partnerOperations = this.Context.UserPartnerOperations;

            this.Context.ConsoleHelper.StartProgress("Getting Subscribed Skus");

            // get customer's subscribed skus information.
            var customerSubscribedSkus = partnerOperations.Customers.ById(selectedCustomerId).SubscribedSkus.Get();
            this.Context.ConsoleHelper.StopProgress();

            // Prepare license request.
            LicenseUpdate updateLicense = new LicenseUpdate();

            // Select the first subscribed sku.
            SubscribedSku sku = customerSubscribedSkus.Items.First();
            LicenseAssignment license = new LicenseAssignment();

            // assigning first subscribed sku as the license
            license.SkuId = sku.ProductSku.Id;
            license.ExcludedPlans = null;

            List<LicenseAssignment> licenseList = new List<LicenseAssignment>();
            licenseList.Add(license);
            updateLicense.LicensesToAssign = licenseList;

            this.Context.ConsoleHelper.StartProgress("Assigning License");

            // Assign licenses to the user.
            var assignLicense = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(selectedCustomerUserId).LicenseUpdates.Create(updateLicense);
            this.Context.ConsoleHelper.StopProgress();

            this.Context.ConsoleHelper.StartProgress("Getting Assigned License");

            // get customer user assigned licenses information after assigning the license.
            var customerUserAssignedLicenses = partnerOperations.Customers.ById(selectedCustomerId).Users.ById(selectedCustomerUserId).Licenses.Get();
            this.Context.ConsoleHelper.StopProgress();

            Console.WriteLine("License was successfully assigned to the user.");
            License userLicense = customerUserAssignedLicenses.Items.First();
            this.Context.ConsoleHelper.WriteObject(userLicense, "Assigned License");
        }
    }
}
