import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am on the {string}", (a) => {
  cy.visit("/show-auction-page");
});

When("I enter Honda into the search box", (a) => {
  cy.getDataTest("search-auction").type("Honda");
});

Then("I should see only cars with the brand {string} in the results", (a) => {
  cy.get("#auction-title").should("contain.text", "Honda Civic Auction");
});
