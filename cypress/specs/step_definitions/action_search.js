import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am on the Action Page", (a) => {
  cy.visit("/show-auction-page");
});

Given("I am on the {string}", (a) => {
  cy.visit("/show-auction-page");
});

When("I enter {string} into the auction page search box", (str) => {
  cy.getDataTest("search-auction").type(str);
});

Then("I should see only cars with the brand {string} in the results", (a) => {
  cy.getDataTest("container").its("length").should("be.gt", 0);
});
