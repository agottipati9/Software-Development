// Implementation by Aashish Gottipati
// C++_Quicksort.cpp : This file contains the 'main' function. Program execution begins and ends there.
//


#include <iostream>
#include <stdio.h>

/* Swaps two elements */
void swap(int* a, int* b) {
	int temp = *a;
	*a = *b;
	*b = temp;
}

/* This function takes the last element as the pivot. The element is placed into its correct pivot location.
All elements smaller are placed to the left of the pivot and all larger elements are placed to the right of the pivot. */
int partition(int arr[], int low, int high) {

	int pivot = arr[high];
	int smaller_index = low - 1;

	for (int curr_index = low; curr_index < high; curr_index++) {

		//If current element is smaller than or equal to pivot
		if (arr[curr_index] <= pivot) {

			// increments the index of the smaller element and swap elements
			smaller_index++;
			swap(&arr[smaller_index], &arr[curr_index]);

		}
	}

	swap(&arr[smaller_index + 1], &arr[high]);
	return(smaller_index + 1);
}

/* Quicksort algoirthm. */
void sort(int arr[], int low, int high) {
	if (low < high) {

		// partition index
		int index = partition(arr, low, high);

		// Recursively sort both halves of the array separately
		sort(arr, low, index - 1);
		sort(arr, index + 1, high);
	}
}

/* Displays the sorted array.*/
void printArray(int arr[], int size) {
	for (int i = 0; i < size; i++) {
		std::cout << " ";
		printf("%d", arr[i]);
	}
}

// Driver method for quicksort
int quicksort() {
	int arr[] = { 10, 7, 8, 9, 1, 5, 101, 32132, 12834, 38, 0, -1, 8 };
	int size = sizeof(arr) / sizeof(arr[0]);
	sort(arr, 0, size - 1);
	printf("Sorted array: \n");
	printArray(arr, size);
	return 0;
}

int main()
{
	quicksort();
}











