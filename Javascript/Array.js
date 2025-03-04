// Array Destructuring

let arr=[1,2,4,5,6];
let hi=["Deepak","Kumar","Sharma"];
let newArray=[...arr,...hi];
console.log(newArray);

// flat Method

let arr1=[1,2,3,[4,5,6,[7,8,9]]];
console.log(arr1.flat(Infinity));
console.log(arr1.flat(1)); // upto which level structuring is to be done
console.log(arr1.flat(2));