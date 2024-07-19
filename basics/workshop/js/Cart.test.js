var Cart = require('./Cart')

test('can create a cart', () => {
  expect(new Cart()).not.toBeNull();
});