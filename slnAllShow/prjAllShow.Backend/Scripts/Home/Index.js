﻿const vm = Vue.createApp({
    //el: '#app',
    //data: {
    //    items: [
    //        { age: 40, first_name: 'Dickerson', last_name: 'Macdonald' },
    //        { age: 21, first_name: 'Larsen', last_name: 'Shaw' },
    //        { age: 89, first_name: 'Geneva', last_name: 'Wilson' },
    //        { age: 38, first_name: 'Jami', last_name: 'Carney' }]
    //}
    data() {
        return {
            items: [
                { age: 40, first_name: 'Dickerson', last_name: 'Macdonald' },
                { age: 21, first_name: 'Larsen', last_name: 'Shaw' },
                { age: 89, first_name: 'Geneva', last_name: 'Wilson' },
                { age: 38, first_name: 'Jami', last_name: 'Carney' }]
        }
    }
})