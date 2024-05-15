import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I am visit the {string} page", (a) => {
  cy.visit("/show-auction-page");
});

When("I select an element from the selector", () => {
  cy.getDataTest("selectedAuction").select("Honda Civic Auction");
});

Then("I should see the details of the selected item", () => {
  cy.getDataTest("2")
    .should("exist")
    .then(($element) => {
      if ($element) {
        cy.log("Element is present");
      } else {
        cy.log("Element is not present");
      }
    });
});

When("I click on the {string} button", (a) => {
  cy.wait(2000);
  cy.getDataTest("back-button").click();
});

Then("I should see all auctions displayed again", () => {
  cy.getDataTest("container").its("length").should("be.gt", 0);
});
