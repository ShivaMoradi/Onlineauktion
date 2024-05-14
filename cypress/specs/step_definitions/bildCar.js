import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am logged in as a user", () => {
  cy.visit("/registering-page");
  cy.login("john", "123_John");
});

Given("I am on the home page", (a) => {
  cy.visit("/");
});
When("I click on a one car", () => {
  cy.getDataTest("countdown-4").click();
});

When("I enter a bid amount greater than the current highest bid", () => {
  cy.get('[data-test="highestbid"]').as("bidText");
  cy.wait(2000);
  cy.get("@bidText")
    .invoke("attr", "value")
    .then(($val) => {
      cy.get('[data-test="bildinput"]').type(parseFloat($val) + 10);
      cy.get('[data-test="submit"]').click();
    });
});

Then("I should see a message confirming my bid was successful", () => {
  let highestBid;
  cy.get('[data-test="highestbid"]').as("bidText");
  cy.get('[data-test="bildinput"]').as("formText");
  cy.wait(2000);
  cy.get("@bidText")
    .invoke("attr", "value")
    .then(($current) => {
      cy.get("@formText").invoke("attr", "value").should("eq", $current);
    });
});
