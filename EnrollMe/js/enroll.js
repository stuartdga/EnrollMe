

var ClassRow = React.createClass({
    render: function() {
        return(
            <tr>
                <td><span id="classname">{this.props.classRow.classname}</span><br /><span id="instructor">{this.props.classRow.instructor}</span></td>
                <td><span id="dayofclass">{this.props.classRow.dayofclass}</span><br /><span id="timeofclass">{this.props.classRow.timeofclass}</span></td>
            </tr>
        );
    },
});

var ClassList = React.createClass({
    render: function() {
        //var rows = this.props.classes.map(function (classRow) {
        //    return <ClassRow key={classRow.classname} classRow={classRow}/>;
        //}).bind(this);
        this.props.classes.forEach(function(classRow) {
            rows.push(<ClassRow key={classRow.classname} classRow={classRow}/>);
        }.bind(this));
        return (
            <div>
                <table className="pure-table">
                    <thead>
                        <tr>
                            <th>Class</th>
                            <th>Date & Time</th>
                        </tr>
                    </thead>
                    <tbody>{rows}</tbody>
                </table>
            </div>
        );
    },
});

var EnrollForm = React.createClass({
    getInitialState: function() {
        return {
            name: '',
            email: '',
            phone: '',
        };
    },
    handleChangeName: function(e) {
        this.setState({name: e.target.value});
    },
    handleChangeEmail: function(e) {
        this.setState({email: e.target.value});
    },
    handleChangePhone: function(e) {
        this.setState({phone: e.target.value});
    },
    handleSubmit: function(e) {
        e.preventDefault();
        alert(this.state.name);
        //add POST here
    },
    render: function() {
        var enrollButton = React.createElement('a', {href: '#', className: 'pure-button pure-button-primary', onClick: this.handleSubmit}, 'Enroll');
        var nameDiv = React.createElement('div', {className: 'formRow'}, "Name:");
        var nameInput = React.createElement('input', {id: 'name', name: 'name', value: this.state.name, onChange: this.handleChangeName});
        var emailDiv = React.createElement('div', {className: 'formRow'}, "Email:");
        var emailInput = React.createElement('input', {id: 'email', name: 'email', value: this.state.email, onChange: this.handleChangeEmail});
        var phoneDiv = React.createElement('div', {className: 'formRow'}, "Phone:");
        var phoneInput = React.createElement('input', {id: 'phone', name: 'phone', value: this.state.phone, onChange: this.handleChangePhone});
        var buttonDiv = React.createElement('div', {className: 'formRow'}, enrollButton);
        return (
            <div>
                <form>
                    {nameDiv}
                    {nameInput}
                    {emailDiv}
                    {emailInput}
                    {phoneDiv}
                    {phoneInput}
                    {buttonDiv}
                </form>
            </div>
        );
    },
});
    
var EnrollComponent = React.createClass({
    getClassList: function() {
        $.ajax({
            url: this.props.url,
            dataType: 'json',
            success: function(data) {
                if (this.isMounted)
                    this.setState({classes: data});
            }.bind(this),
            error: function(xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    componentDidMount: function() {
        this.getClassList();
    },
    render: function() {
        var name = 'asdf';
        var classes = [];
        return (
            <div>
                <ClassList classes={classes}/>
                <EnrollForm />
            </div>
        );
    }
});

//var CLASSES = [
//  {classname: 'Cardio', instructor: 'Chris', dayofclass: 'Monday', timeofclass: '12:00 PM-1:00 PM'},
//];
//React.render(<EnrollComponent classes={CLASSES}/>,document.getElementById('content'));
React.render(<EnrollComponent url='classlist.txt' />,document.getElementById('content'));