import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I login and fill in the auction form inputs", () => {
  cy.login("Desmond", "234_desmond");
  cy.visit("/userpage");
  cy.getDataTest("create-auction-btn").click();
});

When("I enter invalid details", () => {
  cy.getDataTest("title-input").type("Invalid car");
  cy.getDataTest("end-date-input").type("2023-02-12");
  cy.getDataTest("starting-bid-input").type("-100");
  cy.getDataTest("brand-input").type(" ");
  cy.getDataTest("model-input").type(" ");
  cy.getDataTest("year-input").type("1899");
  cy.getDataTest("color-input").type(" ");
  cy.getDataTest("image-url-input").type("invalid-url");
  cy.getDataTest("mileage-input").type("-500");
  cy.getDataTest("engine-type-input").type(" ");
  cy.getDataTest("engine-displacement-input").type("invalid-displacement");
  cy.getDataTest("features-input").type(",");
});

Then("the auction will not be created", () => {
  cy.getDataTest("save-auction-btn").click();
  cy.wait(2000);
  cy.getDataTest("form-errors").should('be.visible'); 
  cy.getDataTest("year-input").siblings(".text-danger").should('contain', 'Invalid year.'); 
});

