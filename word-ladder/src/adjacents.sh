#!/bin/bash

if [ $# -ne 2 ]; then
    echo "Usage: $0 <word_file> <target_word>"
    exit 1
fi

word_file="$1"
target_word="$2"

awk -v target="$target_word" '
function count_diff(str1, str2) {
    diff_count = 0
    for (i = 1; i <= length(str1); i++) {
        if (substr(str1, i, 1) != substr(str2, i, 1)) {
            diff_count++
        }
    }
    return diff_count
}

{
    if (length($0) == length(target) && count_diff($0, target) == 1) {
        print
    }
}
' "$word_file"

