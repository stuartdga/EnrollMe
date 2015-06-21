//var classes = [
//  {ClassId: "", ClassName: "", DayOfClass: "", TimeOfClass : "", Location : "", InstructorId: ""},
//];

var classes = [];

var ClassRow = React.createClass({
    render: function() {
        return(
            <tr>
                <td><span id="classname">{this.props.classRow.ClassName}</span></td>
                <td><span id="dayofclass">{this.props.classRow.DayOfClass}</span><br /><span id="timeofclass">{this.props.classRow.TimeOfClass}</span></td>
            </tr>
        );
    },
});

var ClassesTable = React.createClass({
    render: function() {
        var rows = [];
        console.log("classList prop");
        console.log(this.props.classList);
        this.props.classList.forEach(function(classRow) {
            rows.push(<ClassRow key={classRow.ClassId} classRow={classRow}/>);
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
    }
});


var ClassesComponent = React.createClass({
    getInitialState: function() {
        return {classes: []};
    },
    componentDidMount: function() {
        var self = this;
        $.ajax({
            url: this.props.url,
            dataType: 'json',
            success: function(data) {
                if (this.isMounted) {
                    var returnModel = data.Data.ReturnModel;
                    this.setState({classes: returnModel});
                }
            }.bind(this),
            error: function(xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    //componentDidMount: function() {
    //    var self = this;
    //    this.getClassesTable();
    //},
    render: function() {
        console.log("classes state");
        console.log(this.state.classes);
        return (
            <div>
                <ClassesTable classList={this.state.classes}/>
            </div>
        );
    }
});
React.render(<ClassesComponent url='http://localhost/EnrollMe/api/classes' />,document.getElementById('content'));