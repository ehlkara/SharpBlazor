redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51IjKxzEGigLMcIKUeCegpxTLsycpNJYlzGDGm2wl7XE4rF7BqHzJ8yeTNQpsFWgbszY3eO4LAQITP8Ch9djiyGWy00bA6cEqKW");
    stripe.redirectToCheckout({ sessionId: sessionId });
}