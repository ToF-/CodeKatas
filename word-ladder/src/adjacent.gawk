#!/bin/gawk
BEGIN {
    srce = []
    split("cat", srce, "")
}
{
    line = $0
    if(length(line) == length($1)) {
        split(line, chars, "")
        diffs = 0
        for (i =1; i <= length(line); i++) {
            if (chars[i] != srce[i]) {
                printf("%s\n", line)
            }
        }
    }
}


