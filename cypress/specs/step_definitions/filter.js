import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

/* No duplicate steps, this one already in example_test.js
Given('I am on the homepage', () => {});*/
Given("homepage", () => {
  cy.visit("/");
});
When("I click on the in the model selector", () => {
  cy.getDataTest("selectmodel").select("Toyota");
});

Then("Shows you a list of car model names", () => {
  cy.ggetDataTestet("optionToyota").should("exist");
});

When("When you click on a model name", () => {
  cy.getDataTest("optionToyota").click({ force: true });
});

Then("Shows the cars with the selected model", () => {
  cy.get("#auctioncard").should("exist");
});
