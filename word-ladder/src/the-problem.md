# The Problem
Write a program which, given a list of 5 letter words such as [this one](words.md), a starting word `S` and an target word `T`, displays the shortest _word ladder_ from `S` to `T`, where each step of the ladder is a word that differs from the previous one by one and only one letter.

If a word ladder cannot be found between `S` and `T`, the progra should display `'no path'`.

If `S` or `T` are not in the word list, the program should say so. 

Examples :

```
> wordladder brain cells
brain
braid
brand
brans
brats
beats
belts
bells
cells

> wordladder never again
no path

> wordladder merci thank
merci is not in the list
```
