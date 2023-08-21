# From a List to a Graph
Creating a graph of all words where edges are neighbor relations can take a lot of time if we go naïvely about it: given a word from the list, find all words in the list that are neighbors to this word. For a list of 3000 words, this will require 9 millions comparisons.

Instead we can examine once the list, generating for each word a sublist of it's neighbor groups. For instance the word DOG has 3 neighbor groups : 
- $OG : the group of all words having O and G has 2d and 3d letters
- D$G : the group of all words having D and G has 1st and 3d letters
- DO$ : the group of all words starting with D and O

reading the word DOG, we would write the following pairs: ($OG,DOG),(D$G,DOG),(DO$,DOG)
reading the word COG, we would add the following pairs: ($OG,COG),(C$G,COG),(CO$,COG)

We end up with a (long) list of pairs (neighbor group, word). We can now store this information in a new graph, where each node is either a word or a group, and egdes mean inclusion in a group. 

![example groups](/images/example-groups.png)

dictionary: for each pair that was generated (G,W), we follow this method: 
- if K is not in the dictionary, add it and associate it with W
- if K is already in the dictionary, add the word W to its list of words

Given the example above, and eliminating the neighbor groups containing only one word, we end up with this dictionary:
```
{ CA$→[CAB,CAT]
, CO$→[COG,COT]
, C$T→[CAT,COT]
, DA$→[DAB,DAG,DAM]
, DO$→[DOC,DOG,DOT]
, D$G→[DAG,DOG]
, $AB→[CAB,DAB]
, $OG→[COG,DOG]
, $OT→[COT,DOT] }
```
The dictionary grants a faster access than the searching of a list. To find the neighbors of a given word, we follow this method:
- generate all the neighbor groups of this word,
- for each group present in the dictionary, list its associated words that are different from the source word

Example, the word CAT generates the group $AT, C$T and CA$.
The group $AT doesn't exist (it would contain only CAT)
The neighbors of CAT are CAB and COT

