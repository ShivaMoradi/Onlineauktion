import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

When("I enter {string} into the search box", (a) => {
  cy.get("#search").type("Nissan");
});

Then("I should see only one  cars", (a) => {
  cy.getDataTest("countdown-5").should("exist");
});
