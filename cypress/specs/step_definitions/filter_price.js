import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

/* No duplicate steps, this one already in cardetail.js
Given('I am on the homepage', () => {});*/

When("I click on the in the price selector", () => {
  cy.getDataTest("selectprice").select("20000-30000");
});

Then("Shows you a list of car price", () => {
  cy.getDataTest("priceoption3").should("exist");
});

When("When you click on a price", () => {
  cy.getDataTest("priceoption3").click({ force: true });
});

Then("Shows the cars with the selected price", () => {
  // todo
});
