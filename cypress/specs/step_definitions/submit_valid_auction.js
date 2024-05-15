import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given("I login and fill in the auction form", () => {
  cy.login("Desmond", "234_desmond");
  cy.visit("/userpage");
  cy.getDataTest("create-auction-btn").click();
});

When("I enter valid details", () => {
  cy.getDataTest("title-input").type("Classic Car Auction");
  cy.getDataTest("end-date-input").type("2024-12-31");
  cy.getDataTest("starting-bid-input").type("1000");
  cy.getDataTest("brand-input").type("Ferrari");
  cy.getDataTest("model-input").type("F40");
  cy.getDataTest("year-input").type("1990");
  cy.getDataTest("color-input").type("Red");
  cy.getDataTest("image-url-input").type("https://example.com/car.jpg");
  cy.getDataTest("mileage-input").type("50000");
  cy.getDataTest("engine-type-input").type("V8");
  cy.getDataTest("engine-displacement-input").type("3.0L");
  cy.getDataTest("transmission-select").select("manual");
  cy.getDataTest("features-input").type("Leather seats, Air conditioning");
});

Then("the auction will be created", () => {
  cy.getDataTest("save-auction-btn").click();
  cy.wait(2000);
  cy.on('window:alert', (str) => {
    expect(str).to.equal('The car and auction data is now saved in the database!');
  });
});