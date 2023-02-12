## Problem description
Background

A virtual basket is a common e-commerce feature. Users can add several items to the basket and later pay for all the items at once. Promotional discounts are sometimes available as part of virtual baskets. When certain items or combinations of items are purchased at the same time a discount is applied. Correctly calculating the total price of the items in a basket, including discounts, is important. That is required for displaying the right amount to pay to customers as well as ensuring that the right amount is charged.

Task

Please implement C# code that calculates the total price of a given collection of items. The solution should support applying discount rules.

Specifically, a “buy two, get one free” deal for certain items or combinations of certain items must be implemented

For example, if a “buy two, get one free” promotion is enabled for apples and oranges, when buying two apples and one orange, payment for 2 of the items would be required.

Notes

- The implementation should be a fully working maintainable pricing solution that is of production quality

- This task doesn’t require implementing a shopping basket, an eCommerce web site or a console application

- As a guideline, please aim to spend about two hours working on a solution for this task

- The task and / or your solution shouldn’t be publicly shared, eg. using your GitHub profile or other channel

## Limitations of this implementation that need to be fleshed out
- This implementation does not currently take into account item prices (e.g. if apples and oranges cost different amounts no logic is in place to determine which item should be free, it will just do the first one).
- We currently assume that 241s that span multiple categories can stack (e.g. if a 241 apples and oranges, and a 241 apples and bananas occur, then buying 2 apples gets you one free orange AND one free banana).
- We also assume that a discount can be applied multiple times (e.g. the 241 deal can be applied as many times as possible on one cart)