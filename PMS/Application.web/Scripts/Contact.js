var DummyProfile = [
    {
        "ProfileId": 1,
        "FirstName": "Anand",
        "LastName": "Pandey",
        "Email": "anand@anandpandey.com"
    },
    {
        "ProfileId": 2,
        "FirstName": "John",
        "LastName": "Cena",
        "Email": "john@cena.com"
    }
];


var ProfilesViewModel = function () {
    var self = this;
    var refresh = function () {
        self.Profiles(DummyProfile);
    };

    // Public data properties
    self.Profiles = ko.observableArray([]);
    refresh();
};

ko.applyBindings(new ProfilesViewModel());

self.createProfile = function () {
    alert("Create a new profile");
};

self.editProfile = function (profile) {
    alert("Edit tis profile with profile id as :" + profile.ProfileId);
};

self.removeProfile = function (profile) {
    if (confirm("Are you sure you want to delete this profile?")) {
        self.Profiles.remove(profile);
    }
};




