#!/bin/bash

# This will find all .cs files in the git repository and count them
echo "Total CS files:"
git ls-files '*.cs' | wc -l

# This will find all .cs files in the git repository and count the total lines among them
echo "Total lines in CS files:"
git ls-files '*.cs' | xargs wc -l | awk '{total += $1} END {print total}'

# This will find all .cs files in the git repository, exclude lines with 'interface' keyword, and count the remaining lines
echo "Total lines in CS files (excluding lines with 'interface'):"
git ls-files '*.cs' | xargs grep -v 'interface' | wc -l