### Install URL Rewrite extension

- https://www.iis.net/downloads/microsoft/url-rewrite

### Adding Redirect Rules

- Open IIS Manager
- Navigate to Sites/Default Web Site> URL Rewrite
- Click Add rule(s)
- Name: Redirect for long url

#### Match URL

- Requested URL: Select 'Matches the Pattern'
- Using: select 'Regular Expressions'
- Pattern: [0-9a-zA-Z]{0,20}\$
- Check Ignore Case

#### Action

- Action Type: Redirect

###### Action Properties

- Redirect URL: http://localhost:5001/api/URL/{R:0}
- Redirect Type: Permanent (301)

Apply Rules and Restart IIS
