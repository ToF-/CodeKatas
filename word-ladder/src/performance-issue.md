# Performance Issue
The breadth-first search algorithm works but it's not efficient. The python program, when given a list of about 5700 five-letter words, yields the following when executed with profiling options:
```
> python3 -m cProfile wordladder-list.py www-cs-faculty.stanford.edu_~knuth_sgb-words.txt backs horse
4027 path steps
horse
gorse
goose
goosy
gooky
booky
books
bocks
backs
20809413 comparisons
         20877685 function calls in 21.926 seconds

   Ordered by: cumulative time

   ncalls  tottime  percall  cumtime  percall filename:lineno(function)
        1    0.000    0.000   21.926   21.926 {built-in method builtins.exec}
        1    0.004    0.004   21.926   21.926 wordladder-list.py:1(<module>)
        1    0.016    0.016   21.921   21.921 wordladder-list.py:24(wordLadder)
     3614   19.998    0.006   21.895    0.006 wordladder-list.py:9(adjacents)
 20805799    1.894    0.000    1.894    0.000 {built-in method builtins.len}
    24547    0.007    0.000    0.007    0.000 {method 'get' of 'dict' objects}
    34322    0.004    0.000    0.004    0.000 {method 'append' of 'list' objects}
     3615    0.002    0.000    0.002    0.000 {method 'pop' of 'list' objects}
     5757    0.001    0.000    0.001    0.000 {method 'strip' of 'str' objects}
       11    0.000    0.000    0.000    0.000 {built-in method builtins.print}
        1    0.000    0.000    0.000    0.000 {built-in method io.open}
        6    0.000    0.000    0.000    0.000 <frozen codecs>:319(decode)
        6    0.000    0.000    0.000    0.000 {built-in method _codecs.utf_8_decode}
        1    0.000    0.000    0.000    0.000 {method '__exit__' of '_io._IOBase' objects}
        1    0.000    0.000    0.000    0.000 <frozen codecs>:309(__init__)
        1    0.000    0.000    0.000    0.000 {method 'disable' of '_lsprof.Profiler' objects}
        1    0.000    0.000    0.000    0.000 <frozen codecs>:260(__init__)
```
Showing the word to word comparison in the inner loop of `adjacents` occurred more than 20 million times!
