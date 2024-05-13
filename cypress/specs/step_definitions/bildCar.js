import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am logged in as a user", () => {
  cy.visit("/registering-page");
  cy.login("john", "123_John");
});

Given("I am on the home page", (a) => {
  cy.visit("/");
});
When("I click on a one car", () => {
  cy.get('[data-test="countdown-4"]').click();
});

When("I enter a bid amount greater than the current highest bid", () => {
  const action = { bild: "1100010" };
  cy.get('[data-test="bildinput"]').type(action.bild);
  cy.wait(2000);
  cy.get('[data-test="submit"]').click();
});

Then("I should see a message confirming my bid was successful", () => {
  const action = { bild: "1100010" };
  cy.get('[data-test="highestbid"]').should("contain.text", action.bild);
});
