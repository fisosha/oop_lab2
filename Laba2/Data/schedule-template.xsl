<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<html>
			<head>
				<title>Lecture Schedule</title>
				<style>
					table { border-collapse: collapse; width: 100%; }
					th, td { border: 1px solid black; padding: 8px; text-align: left; }
					th { background-color: #f2f2f2; }
				</style>
			</head>
			<body>
				<h1>Lecture Schedule</h1>
				<table>
					<tr>
						<th>Day</th>
						<th>Time</th>
						<th>Lecturer</th>
						<th>Department</th>
						<th>Room</th>
						<th>Students</th>
					</tr>
					<xsl:for-each select="schedule/lecture">
						<tr>
							<td>
								<xsl:value-of select="@day" />
							</td>
							<td>
								<xsl:value-of select="@time" />
							</td>
							<td>
								<xsl:value-of select="@lecturer" />
							</td>
							<td>
								<xsl:value-of select="@department" />
							</td>
							<td>
								<xsl:value-of select="@room" />
							</td>
							<td>
								<ul>
									<xsl:for-each select="students/student">
										<li>
											<xsl:value-of select="@name" />
											(<xsl:value-of select="@group" />)
										</li>
									</xsl:for-each>
								</ul>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
