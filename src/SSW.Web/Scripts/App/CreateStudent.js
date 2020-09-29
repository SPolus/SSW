var registerStudentModel = Backbone.Model.extend({
    url: 'create',

    defaults: {
        firstName: '',
        lastName: '',
        email: '',
        password: ''
    }
});

var registerStudentView = Backbone.View.extend({
    el: $('#createStudentForm'),

    events: {
        'click #btnCreate': 'register'
    },

    initialize: function () {
        console.log('View: initialization');
        this.listenTo(this.model, 'change', this.render);
    },

    render: function () {

    },

    register: function () {
        console.log('View: register function');

        this.model.set({
            'firstName': $('#firstName').val(),
            'lastName': $('#lastName').val(),
            "email": $('#email').val(),
            "password": $('#password').val()
        });

        console.log('View: register function, model attributes: ' + JSON.stringify(this.model));

        this.model.save(
            this.model.toJSON(),
            {
                success: function (model, response, options) {
                    if (response.success) {
                        location.href = "/Students/Index"
                    } else {
                        alert("Status code: " + response.statusCode + ". " + response.statusCodeText + ". " + response.responseText);
                    }
                    console.log("success\n" + JSON.stringify(model) + "\n\n" + JSON.stringify(response) + "\n\n" + JSON.stringify(options));
                },

                error: function (model, response, options) {
                    console.log("error\n" + JSON.stringify(model) + "\n\n" + JSON.stringify(response) + "\n\n" + JSON.stringify(options));
                }
            }
        );

        console.log('View: register completed');
    }
});

var student = new registerStudentModel();
var view = new registerStudentView({ model: student });
Backbone.history.start();