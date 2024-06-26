import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given('I login using my credentials', () => {
  cy.login("Desmond", "234_desmond");
  cy.wait(1000);
});

Then('I am logged in', () => {
 cy.getDataTest('nav').contains(/userpage/i).should("be.visible");
 cy.getDataTest('nav-userpage').click();
 cy.wait(1000);
});

Then('I logout', () => {
  cy.logout();
});