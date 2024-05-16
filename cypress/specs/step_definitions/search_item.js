import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("Hope Page", () => {
  cy.visit("/");
});

When("I enter {string} into the search box", (str) => {
  cy.get("#search").type(str);
});

Then("I should see only cars with the brand {string} in the result", (a) => {
  cy.getDataTest("countdown-5").should("contain.text", "Nissan");
});
