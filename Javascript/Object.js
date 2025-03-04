const obj={
    name:"John",
    age:30,
    "city":"California",
    isLoggedIn:true,
    hobbies:["music","movies","sports"],
    address:{
        street:"123 Main St",
        city:"New York",
        state:"NY"
    },
    greet:function(){
        return `Hello ${this.name}`;
    },
    sayAge:()=>{
        return `I am ${this.age} years old`;
    }

};


console.log(obj.name);
console.log(obj["name"]);
console.log(obj["city"]);  //city will be only accessible with this syntax only
console.log(obj["address"]["city"]);
// how to call function inside object through [] brackets
console.log(obj.greet());
console.log(obj.sayAge());
console.log(obj["greet"]());

console.log(Object.keys(obj));
console.log(Object.values(obj));