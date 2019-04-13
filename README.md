April 13, 2019: Twin Cities Code Camp 23

<strong>[Queryable Logs: Getting Started with Structured Logging](https://twincitiescodecamp.com/#/talks/1013)</strong>

<em>"In this age of blossoming data analytics, even your logs can be aggregated to find the most important information quickly! This talk will start by showing you an example application using Serilog, one of the top libraries available for .NET. Next we'll demonstrate how to navigate structured logs and how it compares to traditional logging. We will cover libraries available for the different development platforms so you can pick an approach that works for you. If you are interested in taking your logging to the next level, this talk is for you!"</em>

<h2>What is structured logging?</h2>
Structured logs differ from unstructured logs like a pile of wood vs a neat stack of wood - you can still retrieve firewood from the stack but one holds form better than the other.

See: [Structured logging concepts in .NET Series](https://nblumhardt.com/2016/06/structured-logging-concepts-in-net-series-1/)

<em>Demo 1: Troubleshooting scenario</em>

<h2>Basic Concepts</h2>
Structured logs are especially excellent for situations where data is any of the following:
<ol>
  <li>Hierarchy - such as user sessions, web calls, or transversing trees</li>
  <li>Tagged - gives specific interpertation to the data such as units(seconds vs milliseconds) or log levels</li>
  <li>Aggregation - require calculations across the log set</li>
</ol>

Wether you are looking at the logs top down (i.e. for administrative purposes) or bottom up (troubleshooting a specific issue) the additional strucure and context given to the logs makes for a richer story.

Structured logs, with say Serilog, give you the option to take more control over the different steps of creating and using logs.
<ol>
  <li>Capture - what to capture, when, how much, at what level, message queue flushing, protecting sensitive info, dynamically changing what/when messages get logged</li>
  <li>Storage - optimized for retrieval vs write, how long to keep, and changing structure of the messages being logged</li>
  <li>Analysis - who is consuming the logs, choosing a platform for storage/analysis. Many mature software is available such as Elastic/ELK, Seq, or Loggy</li>
</ol>
<em>Demo 2: Sample setup (featuring Serilog & MongoDB)</em>

<h2>Making it work for you</h2>
Platforms specific loggers
<ul>
  <li>.NET - Serilog, NLog</li>
  <li>Java - GoDaddy Logger, Logback</li>
  <li>Python - Python Standard Logging Library, mo-logs, structlog</li>
  <li>JavaScript - JSNLog, structured-log</li>
  <li>Ruby - Ougai</li>
</ul>

Some helpful links for evaluating Serilog vs NLog
<ul>
  <li>[Blog.elmah](https://blog.elmah.io/serilog-vs-nlog)</li>
  <li>[stackify](https://stackify.com/nlog-vs-log4net-vs-serilog)</li>
  <li>[Reddit](https://www.reddit.com/r/dotnet/comments/9cziy4/nlog_vs_log4net_vs_serilog_compare_net_logging)</li>
</ul>

Implementation consideratins for Serilog:
<ul>
  <li><strong>NugGet packages</strong> many to choose from; never had an issues with any of them but thing about each package is another dependancy for your application</li>
  <li><strong>Dependancy Injectinon</strong> Serilog is static, which means to use DI you'll need to either wrap it or use the built in implementation of ILogger (see code example)</li>
  <li><strong>Logger Configuration</strong> LoggerConfiguration can be enriched, but can't add more sinks after it's been created. Consider creating your logger from a configuration (not included in example code)</li>
  <li><strong>Testing</strong> using the ILogger class in .NET, each class needs its own version of ILogger<classname> (see Helper in example code). Consider how this will mock.</li>
</ul>

<em>Demo 3: Implementation gotchas with Serilog in .NET Core & DI</em>

Thank you for attending! 

You can find me, Elsa Vezino, active in these local communities/events:
[Twin Cities .NET User Group](https://www.meetup.com/tcdnug/)

[Speaking at Norwegian Developer Conference Minnesota 2019](https://ndcminnesota.com/talk/everyday-agile-for-one-recipes-that-make-development-for-one-fun/) 

[Scratch mentor at Twin Cities Coder Dojo](https://www.coderdojotc.org/)
