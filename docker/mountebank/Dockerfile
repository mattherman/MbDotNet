FROM mhart/alpine-node

EXPOSE 2525
ENTRYPOINT ["mb"]
CMD ["start","--mock"]
ENV MOUNTEBANK_VERSION=1.7.2

RUN apk add --update nodejs
RUN npm install -g mountebank@${MOUNTEBANK_VERSION} --production