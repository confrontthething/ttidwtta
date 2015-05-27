#!/usr/bin/env python
#
# Copyright 2007 Google Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#
import webapp2
import json
from google.appengine.ext import db
from datetime import datetime

class EndChoice():
    frinchfry = 0
    theThing = 1

class Telemetry(db.Model):
    endChoice = db.IntegerProperty(required=True)
    timeSeconds = db.IntegerProperty(required=True)
    date = db.DateTimeProperty(required=True)
    category = db.IntegerProperty(required=True)
    poofGuard = db.BooleanProperty(required=False)
    deaths = db.IntegerProperty(required=False)
    simonSaysErrors = db.IntegerProperty(required=False)
    machineActivations = db.IntegerProperty(required=False)
    voicemailSaved = db.BooleanProperty(required=False)

class MainHandler(webapp2.RequestHandler):
    def get(self):
        self.response.headers.add_header("Content-Type", "application/json")
        self.response.write(json.dumps({ "success" : True }))

class TelemetryHandler(webapp2.RequestHandler):
    def post(self):
        requestData = json.loads(self.request.body)
        category = requestData["category"] if "category" in requestData else 0
        response = {}
        try:
            telemetry = Telemetry(
                endChoice = requestData["endChoice"],
                timeSeconds = requestData["timeSeconds"],
                date = datetime.utcnow(),
                category = category
            )
            if "poofGuard" in requestData:
                telemetry.poofGuard = requestData["poofGuard"]
            if "deaths" in requestData:
                telemetry.deaths = requestData["deaths"]
            if "simonSaysErrors" in requestData:
                telemetry.simonSaysErrors = requestData["simonSaysErrors"]
            if "machineActivations" in requestData:
                telemetry.machineActivations = requestData["machineActivations"]
            if "voicemailSaved" in requestData:
                telemetry.voicemailSaved = requestData["voicemailSaved"]
            key = telemetry.put()
            response = { "success" : True, "key" : key.id() }
        except Exception as e:
            response = { "success" : False, "error" : str(e) }

        self.response.headers.add_header("Content-Type", "application/json")
        self.response.write(json.dumps(response))

    def get(self, key):
        response = {}
        try:
            telemetry = Telemetry.get_by_id(long(key))
            if telemetry == None:
                raise RuntimeError("key {0} not found".format(long(key)))
            response = {
                         "success" : True,
                         "telemetry" : {
                                         "endChoice" : telemetry.endChoice,
                                         "timeSeconds" : telemetry.timeSeconds,
                                         "date" : telemetry.date.isoformat(),
                                         "category" : telemetry.category
                         }
            }
            if telemetry.poofGuard != None:
                response["telemetry"]["poofGuard"] = telemetry.poofGuard
            if telemetry.deaths != None:
                response["telemetry"]["deaths"] = telemetry.deaths
            if telemetry.simonSaysErrors != None:
                response["telemetry"]["simonSaysErrors"] = \
                    telemetry.simonSaysErrors
            if telemetry.machineActivations != None:
                response["telemetry"]["machineActivations"] = \
                    telemetry.machineActivations
            if telemetry.voicemailSaved != None:
                response["telemetry"]["voicemailSaved"] = \
                    telemetry.voicemailSaved
        except Exception as e:
            response = { "success" : False, "error" : str(e) }

        self.response.headers.add_header("Content-Type", "application/json")
        self.response.write(json.dumps(response))

class ReportHandlerBase(webapp2.RequestHandler):
    def telemetryInCategory(self, category):
        q = Telemetry.all()
        if not category is None:
            q.filter("category =", category)
        return q

    def getReport(self, category):
        endChoiceReport = {}

        try:
            longCategory = None if category == None else long(category)
            q = self.telemetryInCategory(longCategory)
            q.filter("endChoice =", EndChoice.frinchfry)
            endChoiceReport[EndChoice.frinchfry] = q.count()

            q = self.telemetryInCategory(longCategory)
            q.filter("endChoice =", EndChoice.theThing)
            endChoiceReport[EndChoice.theThing] = q.count()

            response = {
                "success" : True,
                "endChoice" : {
                    "frinchfry" : endChoiceReport[EndChoice.frinchfry],
                    "theThing" : endChoiceReport[EndChoice.theThing]
                }
            }
        except Exception as e:
            response = { "success" : False, "error" : str(e) }

        self.response.headers.add_header("Content-Type", "application/json")
        self.response.write(json.dumps(response))

class ReportHandlerGeneral(ReportHandlerBase):
    def get(self):
        self.getReport(None)

class ReportHandlerSpecific(ReportHandlerBase):
    def get(self, category):
        self.getReport(category)


app = webapp2.WSGIApplication([
  webapp2.Route("/", handler=MainHandler),
  webapp2.Route("/telemetry", handler=TelemetryHandler),
  webapp2.Route("/telemetry/<key>", handler=TelemetryHandler),
  webapp2.Route("/report/all", handler=ReportHandlerGeneral),
  webapp2.Route("/report/<category>", handler=ReportHandlerSpecific)
], debug=True)
