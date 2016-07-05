// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Samples
{
    using Context;
    using CustomerDirectoryRoles;
    using Customers;
    using CustomerSubscribedSkus;
    using CustomerUser;
    using IndirectPartners;
    using Invoice;
    using Models.Auditing;
    using Models.Customers;
    using Offers;
    using Orders;
    using Profile;
    using RatedUsage;
    using ServiceRequests;
    using Subscriptions;

    /// <summary>
    /// The main program class for the partner center .NET SDK samples.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry function.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        public static void Main(string[] args)
        {
            var context = new ScenarioContext();

            IPartnerScenario[] mainScenarios = new[]
            {
                Program.GetCustomerScenarios(context),
                Program.GetIndirectPartnerScenarios(context),
                Program.GetOfferScenarios(context),
                Program.GetOrderScenarios(context),
                Program.GetSubscriptionScenarios(context),
                Program.GetRatedUsageScenarios(context),
                Program.GetServiceRequestsScenarios(context),
                Program.GetInvoiceScenarios(context),
                Program.GetProfileScenarios(context),
                Program.GetCustomerUserScenarios(context),
                Program.GetCustomerSubscribedSkus(context),
                Program.GetCustomerDirectoryRolesScenarios(context),
                Program.GetAuditingScenarios(context)
            };

            // run the main scenario
            new AggregatePartnerScenario("Partner SDK samples", mainScenarios, context).Run();
        }

        /// <summary>
        /// Gets the Customer Directory Roles scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The Customer Directory Roles scenarios.</returns>
        private static IPartnerScenario GetCustomerDirectoryRolesScenarios(ScenarioContext context)
        {
            var customerDirectoryRolesScenarios = new IPartnerScenario[]
            {
                new GetCustomerDirectoryRoles(context),
                new AddUserMemberToDirectoryRole(context),
                new GetCustomerDirectoryRoleUserMembers(context),
                new RemoveCustomerUserMemberFromDirectoryRole(context)
            };

            return new AggregatePartnerScenario("Customer Directory Roles", customerDirectoryRolesScenarios, context);
        }

        /// <summary>
        /// Gets the Customer Subscribed SKUs scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The Customer Subscribed SKUs scenarios.</returns>
        private static IPartnerScenario GetCustomerSubscribedSkus(ScenarioContext context)
        {
            var customerSubscribedSkusScenarios = new IPartnerScenario[]
            {
                new GetCustomerSubscribedSkus(context)
            };

            return new AggregatePartnerScenario("Customer Subscribed Skus", customerSubscribedSkusScenarios, context);
        }

        /// <summary>
        /// Gets the customer user scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The customer user scenarios.</returns>
        private static IPartnerScenario GetCustomerUserScenarios(ScenarioContext context)
        {
            var customerUserScenarios = new IPartnerScenario[]
            {
                new GetCustomerUserCollection(context),
                new CreateCustomerUser(context),
                new DeleteCustomerUser(context),
                new GetCustomerUserDetails(context),
                new UpdateCustomerUser(context),
                new GetPagedCustomerUsers(context),
                new GetCustomerUserDirectoryRoles(context),
                new CustomerUserAssignedLicenses(context),
                new CustomerUserAssignLicenses(context) 
            };

            return new AggregatePartnerScenario("Customer User samples", customerUserScenarios, context);
        }

        /// <summary>
        /// Gets the customer scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The customer scenarios.</returns>
        private static IPartnerScenario GetCustomerScenarios(IScenarioContext context)
        {
            var customerFilteringScenarios = new IPartnerScenario[]
            {
                new FilterCustomers("Filter by company name", CustomerSearchField.CompanyName, context),
                new FilterCustomers("Filter by domain name", CustomerSearchField.Domain, context)
            };

            var customerScenarios = new IPartnerScenario[]
            {
                new CreateCustomer(context),
                new CheckDomainAvailability(context),
                new GetPagedCustomers(context, context.Configuration.Scenario.CustomerPageSize),
                new AggregatePartnerScenario("Customer filtering", customerFilteringScenarios, context),
                new GetCustomerDetails(context),
                new DeleteCustomerFromTipAccount(context),
                new GetCustomerManagedServices(context),
                new GetCustomerRelationshipRequest(context),
                new UpdateCustomerBillingProfile(context),
                new ValidateCustomerAddress(context)
            };

            return new AggregatePartnerScenario("Customer samples", customerScenarios, context);
        }

        /// <summary>
        /// Gets the indirect partner scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The indirect partner scenarios.</returns>
        private static IPartnerScenario GetIndirectPartnerScenarios(IScenarioContext context)
        {
            return new AggregatePartnerScenario(
                "Indirect partner samples",
                new IPartnerScenario[] { new VerifyPartnerMpnId(context), new GetSubscriptionsByMpnId(context) },
                context);
        }

        /// <summary>
        /// Gets the offer scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The offer scenarios.</returns>
        private static IPartnerScenario GetOfferScenarios(IScenarioContext context)
        {
            var offerScenarios = new IPartnerScenario[]
            {
                new GetOffer(context),
                new GetOfferCategories(context),
                new GetOffers(context),
                new GetPagedOffers(context, context.Configuration.Scenario.DefaultOfferPageSize),
            };

            return new AggregatePartnerScenario("Offer samples", offerScenarios, context);
        }

        /// <summary>
        /// Gets the order scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The order scenarios.</returns>
        private static IPartnerScenario GetOrderScenarios(IScenarioContext context)
        {
            var orderScenarios = new IPartnerScenario[]
            {
                new CreateOrder(context),
                new GetOrderDetails(context),
                new GetOrders(context)
            };

            return new AggregatePartnerScenario("Order samples", orderScenarios, context);
        }

        /// <summary>
        /// Gets the subscription scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The subscription scenarios.</returns>
        private static IPartnerScenario GetSubscriptionScenarios(IScenarioContext context)
        {
            var subscriptionScenarios = new IPartnerScenario[]
            {
                new GetSubscription(context),
                new GetSubscriptions(context),
                new GetSubscriptionsByOrder(context),
                new UpdateSubscription(context),
                new UpgradeSubscription(context),
                new AddSubscriptionAddOn(context)
            };

            return new AggregatePartnerScenario("Subscription samples", subscriptionScenarios, context);
        }

        /// <summary>
        /// Gets the rated usage scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The rated usage scenarios.</returns>
        private static IPartnerScenario GetRatedUsageScenarios(IScenarioContext context)
        {
            var ratedUsageScenarios = new IPartnerScenario[]
            {
                new GetCustomerUsageSummary(context),
                new GetCustomerSubscriptionsUsage(context),
                new GetSubscriptionResourceUsage(context),
                new GetSubscriptionUsageRecords(context),
                new GetSubscriptionUsageSummary(context)
            };

            return new AggregatePartnerScenario("Rated usage samples", ratedUsageScenarios, context);
        }

        /// <summary>
        /// Gets the service request scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The service request scenarios.</returns>
        private static IPartnerScenario GetServiceRequestsScenarios(IScenarioContext context)
        {
            var serviceRequestsScenarios = new IPartnerScenario[]
            {
                new CreatePartnerServiceRequest(context),
                new GetCustomerServiceRequests(context),
                new GetPagedPartnerServiceRequests(context, context.Configuration.Scenario.ServiceRequestPageSize),
                new GetPartnerServiceRequestDetails(context),
                new GetServiceRequestSupportTopics(context),
                new UpdatePartnerServiceRequest(context)
            };

            return new AggregatePartnerScenario("Service request samples", serviceRequestsScenarios, context);
        }

        /// <summary>
        /// Gets the invoice scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The invoice scenarios.</returns>
        private static IPartnerScenario GetInvoiceScenarios(IScenarioContext context)
        {
            var invoiceScenarios = new IPartnerScenario[]
            {
                new GetAccountBalance(context),
                new GetInvoice(context),
                new GetInvoiceLineItems(context, context.Configuration.Scenario.InvoicePageSize),
                new GetPagedInvoices(context)
            };

            return new AggregatePartnerScenario("Invoice samples", invoiceScenarios, context);
        }

        /// <summary>
        /// Gets the profile scenarios.
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The invoice scenarios.</returns>
        private static IPartnerScenario GetProfileScenarios(IScenarioContext context)
        {
            var profileScenarios = new IPartnerScenario[]
            {
                new GetBillingProfile(context),
                new GetLegalBusinessProfile(context),
                new GetOrganizationProfile(context),
                new GetMPNProfile(context),
                new GetSupportProfile(context),
                new UpdateBillingProfile(context),
                new UpdateLegalBusinessProfile(context),
                new UpdateOrganizationProfile(context),
                new UpdateSupportProfile(context)
            };

            return new AggregatePartnerScenario("Partner profile samples", profileScenarios, context);
        }

        /// <summary>
        /// Gets the auditing scenarios
        /// </summary>
        /// <param name="context">A scenario context.</param>
        /// <returns>The auditing scenarios.</returns>
        private static IPartnerScenario GetAuditingScenarios(IScenarioContext context)
        {
            var profileScenarios = new IPartnerScenario[]
            {
                new QueryAuditRecords(context),
                new SearchAuditRecords("Filter by company name", AuditRecordSearchField.CompanyName, context)
            };

            return new AggregatePartnerScenario("Auditing samples", profileScenarios, context);
        }
    }
}
