#!/bin/bash

# Navigate to src directory
cd ./src

# This will find all .cs files in './src' directory of the git repository and count them
echo "Total CS files in ./src:"
total_cs_files=$(git ls-files '*.cs' | wc -l)
echo $total_cs_files

# This will find all .cs files in the './src' directory inside "Interfaces" directories and count them
echo "Total CS files in 'Interfaces' directories in ./src:"
interface_files=$(find . -type d -iname "Interfaces" -exec find '{}' -name "*.cs" \; | wc -l)
echo $interface_files

# This will count the total number of lines in all .cs files in './src' directory of the git repository
echo "Total lines in CS files in ./src:"
total_lines=$(git ls-files '*.cs' | xargs wc -l | awk '{total += $1} END {print total}')
echo $total_lines

# This will count the total number of lines in all .cs files located in "Interfaces" directories inside './src'
echo "Total lines in 'Interfaces' directories in ./src:"
interface_lines=$(find . -type d -iname "Interfaces" -exec find '{}' -name "*.cs" \; | xargs wc -l | awk '{total += $1} END {print total}')
echo $interface_lines

# This will subtract the number of lines in "Interfaces" directories from the total lines in all .cs files inside './src'
echo "Total lines in CS files in ./src (excluding those in 'Interfaces' directories):"
total_lines_excluding_interfaces=$((total_lines - interface_lines))
echo $total_lines_excluding_interfaces