import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am logged", () => {
  cy.visit("/");
  cy.login("john", "123_John");
});

Given("I am on the Auction page", (a) => {
  cy.visit("/show-auction-page");
});

When("I click on the selector option", () => {
  cy.getDataTest("selectedAuction").select("Toyota Corolla Auction");
});

When("I enter a bid amount", () => {
  cy.getDataTest("bidamount").type("444450");
});

Then("I should see the new bid amount displayed", () => {
  cy.wait(3000);
  cy.getDataTest("buttonbid").click();
  cy.getDataTest("highestbid").should("contain.text", "444450");
});
