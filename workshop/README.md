# Workshop

Today you will be TDD'ing a shopping cart.

## Getting started

1. Clone this repository: `git clone git@github.com:dburriss/TestingWorkshop.git`
1. In this directory (the *workshop* directory) you will find starter projects for [C#](csharp/) ,[F#](fsharp/), and [JS](js/). Pick which is your preferred language and open it in your favorite editor.

## Instructions

1. Implement each feature before you move onto the next one (do not read ahead)
1. You can ask colleagues or the instructor for help at any time
1. You must have a failing test for any new code you write
1. Try implement each feature in the most simple way possible
1. Do not forget to go back and refactor once a test is passing

## Assignment

Your team has been tasked with creating a new shopping cart experience. As this is a critical section of the website, as a team you have decided to TDD it as you have heard it lowers the occurrences of defects.

*Basic definition of a product model:*

```
data Product = {
    Id:unique identifier
    Code:6 letter alphanumeric
    Description:text
    Amount:number with decimal
    Version:integer
}
```

### Part 1

Warming up. Implementing the basic cart functionality.

1. As a customer I want to be able to add a product to my shopping cart so that I can carry on shopping and pay for it later
1. As a customer I want to be able to remove a product from my shopping cart so that I can change my mind on what I want
1. As a customer I want to be able to be able to increment the quantity of any item in my cart
1. As a customer I want to be able to be able to decrement the quantity of any item in my cart
1. As a customer I want to be able to be able to set the quantity of any item in my cart
1. As a customer I want to be able to be able to see the total value of the current items in my cart

### Part 2

Dealing with dependencies. We want visibility on what is happening on the cart. We want any changes to the contents of the cart to be logged.

1. Create a logger abstraction to be used
1. Make sure the logger is called when a product is added, removed, or changes quantity.

### Part 3

Dealing with change. There is an external API that allows you as a developer to POST a list of products to with quantities. You will get back the discounts that apply to the Product Lines as well as those applying to the cart as a whole.

*Definition of Discounts model*

```
data ItemDiscount = (PercentagePerProduct is percentage) OR (PercentagePerProductLine is percentage)

data ItemDiscountResult = {
    ProductId:unique identifier
    Discount: ItemDiscount
}

data CartDiscountResult is percentage

data Discount = ItemDiscountResult OR CartDiscountResult

data DiscountResult is list of Discount
```

1. As a customer I want any discounts that apply to items in my cart to reflect in the item price and total price
1. As a customer I want to see the amount of the discount on a Product Line so I know how much I am saving per line
1. As a customer I want to see the amount of the discount due to the total contents of my cart so I know how much I am saving in total