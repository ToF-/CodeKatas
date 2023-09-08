# The Problem
Write a program which, given a list of 5 letter words such as [this one](https://www-cs-faculty.stanford.edu/~knuth/sgb-words.txt), a starting word _S_ and an target word _T_, displays the shortest word ladder from _S_ to _T_, where each step of the ladder is a transition from a word to an _adjacent_ word, where two words are said adjacent if they differ by one and only one letter. 

If a word ladder cannot be found between _S_ and _T_, the programm should display 'no path'.

If _S_ or _T_ are not in the word list, the program should say so. 

Examples :

```
> wordladder brain cells
brain braid brand brans brats beats belts bells cells

> wordladder never again
no path

> workladder merci thank
merci is not in the list
```
