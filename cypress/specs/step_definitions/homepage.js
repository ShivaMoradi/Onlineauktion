import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am on the {string} page", (a) => {
  cy.visit("/");
});

Then("I should then see the price of the car.", () => {
  cy.getDataTest("cotainercars").its("length").should("be.gt", 0);
});
