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
  cy.getDataTest("form-errors").should('be.visible'); // Check for form error alert
  cy.getDataTest("year-input").siblings(".text-danger").should('contain', 'Invalid year.'); // Check for specific error message
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
  cy.on('window:alert', (str) => {
    expect(str).to.equal('The car and auction data is now saved in the database!');
  });
});
