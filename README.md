# Hiking Plan Optimizer

## Introduction

Hello, I am Anna, and this is my solution for the following task!

## Task Description

We want to plan a hiking trip where we have several segments, and we want to walk as much as necessary each day, but no more. 

The task involves multiple challenges:
- Handling the input from a file and reacting to possible errors
- Dividing the paths optimally into the smallest possible daily segments
- Ensuring the solution is efficient, especially in certain cases
- Generating a proper and user-friendly output

The core challenge was the optimal division of the paths. I decided to use a binary search to find the optimal maximum length for one day's hike. This approach allows me to prepare the path array for the final planned hike efficiently.

## Example

Assuming the input file contains:
<br>
6<br>
3<br>
11<br>
16<br>
5<br>
5<br>
12<br>
10<br>
<br>

The output will be:
<br>
Your plan:<br>
<br>
Day 1: 11 km<br>
Day 2: 26 km<br>
Day 3: 22 km<br>
<br>
Maximum: 26 km<br>
<br>

## Conclusion

This project demonstrates my approach to solving the hiking plan optimization problem, focusing on efficient algorithm design, robust input handling, and clear output generation. I hope this provides a clear insight into my problem-solving skills and coding practices.
