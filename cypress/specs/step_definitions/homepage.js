import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am on the {string} page", (a) => {
  cy.visit("/");
});

When("the page loads", () => {
  cy.wait("http://localhost:5173").its("response.statusCode").should("eq", 200);
});

Then("I should then see the price of the car.", () => {
  cy.getDataTest("cotainercars").its("length").should("be.gt", 0);
});
