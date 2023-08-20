# The Problem
Write a program which, given a list of words such as the ones contained in the files *3-letter-words.txt*, *4-letter-words.txt* or *5-letter-words.txt*, a starting word _S_ and an target word _T_, yields the shortest word ladder from _S_ to _T_, where each step of the ladder is a transition from a word to an adjacent word, and two words are said adjacent if they differ by one and only one letter.

Examples :

```
> ladder 3-letter-words.txt DOG CAT
DOG DOT COT CAT

> ladder 4-letter-words.txt WARM COLD
WARM WARD CARD CORD COLD
```


