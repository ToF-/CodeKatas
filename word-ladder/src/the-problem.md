# The Problem
Write a program which, given a list of words such as the ones listed in the last section, a starting word _S_ and an target word _T_, yields the shortest word ladder from _S_ to _T_, where each step of the ladder is a transition from a word to an _adjacent_ word, where two words are said adjacent if they differ by one and only one letter.

Examples :

```
> wordladder  words.txt dog cat
dog dot cot cat

> wordladder words.txt warm cold
warm ward card cord cold
```


