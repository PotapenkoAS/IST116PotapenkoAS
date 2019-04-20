<%@ page contentType="text/html;" isELIgnored="false" %>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<html>
<body onload="val()">
<div style="display: flex">
    <div align="left">
        <table border="1" cellpadding="3">
            <caption><h2>List of users</h2></caption>
            <tr>
                <th>Surname</th>
                <th>Name</th>
                <th>Action</th>
            </tr>
            <c:forEach var="worker" items="${workers}">
                <tr>
                    <td><c:out value="${worker.surname}"/></td>
                    <td><c:out value="${worker.name}"/></td>
                    <td><a href="./up/${worker.workerID}">Update</a></td>
                </tr>
            </c:forEach>
        </table>
        <p style="font-size: larger; color: red">${text}</p>
    </div>

    <script>
        function val() {
            var tmp = "${history.size()}";
            if (tmp < 1) {
                document.getElementById("historyTableDiv").style.display = "none";
            }
        }
    </script>

    <div align="right" id="historyTableDiv">
        <table border="1" cellpadding="3">
            <caption><h2>History</h2></caption>
            <tr>
                <th>Surname</th>
                <th>Name</th>
            </tr>
            <c:forEach var="worker" items="${history}">
                <tr>
                    <td><c:out value="${worker.surname}"/></td>
                    <td><c:out value="${worker.name}"/></td>
                </tr>
            </c:forEach>
        </table>
    </div>
</div>
<a href="${pageContext.request.contextPath}/add">ADD</a>
<p>Singleton message: ${message}</p>
</body>
</html>
