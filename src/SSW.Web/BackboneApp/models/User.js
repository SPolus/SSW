import Backbone from 'backbone';
var app = app || {};

app.User = Backbone.Model.extend({
   defaults: {
       Email: null,
       Password: null
   } 
});
