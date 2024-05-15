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
  let newBild;
  cy.getDataTest("highestbid")
    .invoke("attr", "value")
    .then(($bid) => {
      newBild = parseInt($bid) + 10;
    })
    .then(() => {
      cy.getDataTest("bidamount").type(newBild);
      cy.getDataTest("buttonbid").click();
    });
});

Then("I should see the new bid amount displayed", () => {
  let newValue;
  cy.wait(2000);
  cy.getDataTest("highestbid")
    .invoke("attr", "value")
    .then(($val) => {
      newValue = $val;
    })
    .then(() => {
      cy.getDataTest("highestbid").should("contain.text", newValue);
    });
});
