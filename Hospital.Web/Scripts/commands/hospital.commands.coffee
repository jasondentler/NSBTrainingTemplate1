# @reference ~/Scripts/lib/jquery.js
# @reference ~/Scripts/lib/knockout.js
# @reference ~/Scripts/lib/knockout-mapping.js
# @reference ~/Scripts/lib/jquery.signalR.js
# @reference ~/signalrhubs
# @reference ~/Scripts/app/hospital.js
# @reference ~/content/jquery.pnotify.default.css

window.commandHub = null

window.createUUID = `function () {
    // http://www.ietf.org/rfc/rfc4122.txt
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
    s[8] = s[13] = s[18] = s[23] = "-";

    var uuid = s.join("");
    return uuid;
}`

hubInitializers.push -> 
	commandHub = $.connection.commandHub
	window.commandHub = commandHub;

	pendingCommands = new Object()

	commandHub.commandSent = (type, text, id) ->
		console.log "Command Sent"
		console.log "Type: " + type
		console.log "Text: " + text
		console.log "Command Id: " + id
		cmdNotify = $.pnotify
			pnotify_title: "Pending Command"
			pnotify_text: text
			pnotify_hide: false
			pnotify_sticker: false
			pnotify_close: false
		pendingCommands['cmd' + id] = cmdNotify
		return

	commandHub.commandProcessed = (id) ->
		console.log "Command Processed"
		console.log "Command Id: " + id
		cmdNotify = pendingCommands['cmd' + id]
		return if !cmdNotify?
		$(cmdNotify).fadeOut "slow", -> cmdNotify.pnotify_remove()
		return

	return
