

## Limitations of this implementation that need to be fleshed out
- This implementation does not currently take into account item prices (e.g. if apples and oranges cost different amounts no logic is in place to determine which item should be free, it will just do the first one).
- We currently assume that 241s that span multiple categories can stack (e.g. if a 241 apples and oranges, and a 241 apples and bananas occur, then buying 2 apples gets you one free orange AND one free banana).
- We also assume that a discount can be applied multiple times (e.g. the 241 deal can be applied as many times as possible on one cart)