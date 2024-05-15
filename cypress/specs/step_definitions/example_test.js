import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am on the homepage", () => {
  cy.visit("/");
  cy.location("pathname").should("equal", "/");
});

When("I click on the contact page link", () => {
  cy.getDataTest("nav-contact-page").click();
});

Then("the contact page should be displayed", () => {
  cy.location("pathname").should("equal", "/contact-page");
  cy.contains(/your email/i).should("be.visible");
});

When("I click on the show auction page link", () => {
  cy.getDataTest("nav-show-auction-page").click();
});

Then("the show auction page should be displayed", () => {
  cy.location("pathname").should("equal", "/show-auction-page");
});
